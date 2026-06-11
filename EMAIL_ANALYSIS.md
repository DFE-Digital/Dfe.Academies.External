# Email Functionality Analysis

## Executive Summary

This codebase uses **GOV.UK Notify** (a SaaS email notification service) for all email sending functionality. Unlike traditional SMTP-based implementations, emails are sent via HTTP API calls to the GOV.UK Notify service using the official .NET client library (`Notify.Client`).

**Key Characteristics:**
- **Technology**: GOV.UK Notify API (not SMTP/SendGrid/MailKit)
- **Execution Pattern**: Asynchronous (`async/await`) but **synchronous within request pipeline** (no queuing)
- **Processing**: Real-time email sending - emails are sent immediately during HTTP request processing
- **Exception Handling**: Minimal - relies on ASP.NET Core middleware for unhandled exceptions
- **Test Mode**: Active filtering restricts emails to `@education.gov.uk` domain in test mode

**Current Implementation Status:**
- ✅ Uses modern async/await patterns
- ✅ Template-based system (no hardcoded email content)
- ✅ Factory pattern for template selection
- ⚠️ No exception handling around email sending
- ⚠️ No retry logic for API failures
- ⚠️ No background processing/queuing mechanism
- ⚠️ Limited error logging

---

## Architecture Overview

### High-Level Flow

```
HTTP Request (Razor Page)
    ↓
User Action Triggers Email (e.g., Add Contributor, Submit Feedback)
    ↓
Service Layer (EmailNotificationService or ContributorEmailSenderService)
    ↓
GOV.UK Notify Client (IAsyncNotificationClient)
    ↓
GOV.UK Notify API (External SaaS Service)
    ↓
Email Delivered
```

### Component Architecture

```
┌─────────────────────────────────────────────────────────┐
│              Razor Pages (Trigger Points)              │
│  - AddAContributor.cshtml.cs                           │
│  - Problem.cshtml.cs                                   │
│  - Feedback.cshtml.cs                                  │
│  - ApplicationHelp.cshtml.cs                            │
│  - DeleteApplication.cshtml.cs                         │
└─────────────────┬───────────────────────────────────────┘
                  │
                  ↓
┌─────────────────────────────────────────────────────────┐
│         ContributorEmailSenderService                   │
│  (For contributor invitation emails only)                │
│  - Maps EmailVariablesDto → Dictionary<string, dynamic> │
│  - Uses ContributorNotifyTemplateFactory                │
└─────────────────┬───────────────────────────────────────┘
                  │
                  ↓
┌─────────────────────────────────────────────────────────┐
│           EmailNotificationService                       │
│  (Core email sending service)                           │
│  - Implements IEmailNotificationService                 │
│  - Wraps IAsyncNotificationClient                        │
│  - Handles test mode filtering                          │
└─────────────────┬───────────────────────────────────────┘
                  │
                  ↓
┌─────────────────────────────────────────────────────────┐
│      IAsyncNotificationClient                            │
│  (GOV.UK Notify .NET Client)                            │
│  - SendEmailAsync() method                              │
└─────────────────┬───────────────────────────────────────┘
                  │
                  ↓
            GOV.UK Notify API
```

---

## Core Components

### 1. EmailNotificationService

**File**: `Dfe.Academies.External.Web/Services/EmailNotificationService.cs`

**Purpose**: Core service that sends emails via GOV.UK Notify API.

**Implementation Details**:
- Implements `IEmailNotificationService` interface
- Uses `IAsyncNotificationClient` (from `Notify.Client` library)
- Configuration-driven API key: `emailnotifications:key`
- Test mode support: When `emailnotifications:testmode` is `true`, only sends to `@education.gov.uk` addresses

**Key Method**:
```csharp
public async Task SendAsync(MessageDto message)
{
    if (this.TestMode && !message.EmailAddress.ToLower().EndsWith("@education.gov.uk")) {
        // Silent failure in test mode
        return;
    }

    EmailNotificationResponse response = await _notificationClient.SendEmailAsync(
        message.EmailAddress, 
        message.TemplateId, 
        message.Personalisation, 
        message.Reference, 
        message.EmailReplyToId);

    _logger.LogInformation($"Email successfully Sent to:- {message.EmailAddress}");
}
```

**Dependencies**:
- `IAsyncNotificationClient` - Injected via constructor
- `IConfiguration` - For reading API key and test mode setting
- `ILogger<BespokeExceptionHandlingMiddleware>` - For logging

**Issues Identified**:
1. ❌ **No exception handling**: `SendEmailAsync` call is not wrapped in try-catch
2. ❌ **No retry logic**: If API call fails, exception propagates up
3. ❌ **Limited logging**: Only logs success, not failures
4. ❌ **Silent test mode failure**: Returns early without logging when test mode blocks email

**Dependency Injection**:
- Registered as **Singleton** in `Program.cs` (line 196)
- Lifetime: Application-scoped (single instance for entire application lifetime)

---

### 2. ContributorEmailSenderService

**File**: `Dfe.Academies.External.Web/Services/ContributorEmailSenderService.cs`

**Purpose**: Specialized service for sending contributor invitation emails. Wraps `EmailNotificationService` with business logic for template selection and data mapping.

**Implementation Details**:
- Implements `IContributorEmailSenderService` interface
- Uses factory pattern to select appropriate template based on:
  - `ApplicationTypes` (FormAMat or JoinAMat)
  - `SchoolRoles` (ChairOfGovernors or other roles)
- Uses AutoMapper to convert `EmailVariablesDto` to `Dictionary<string, dynamic>` for template personalization

**Key Method**:
```csharp
public async Task SendInvitationToContributor(
    ApplicationTypes applicationType,
    SchoolRoles contributorRole, 
    string contributorEmailAddress,
    EmailVariablesDto emailVariables)
{
    var template = _contributorNotifyTemplateFactory.Get(applicationType, contributorRole);
    var personalisation = _mapper.Map<Dictionary<string, dynamic>>(emailVariables);
    
    var message = new MessageDto(contributorEmailAddress, template.TemplateId) 
    { 
        Personalisation = personalisation 
    };
    await _emailNotificationService.SendAsync(message);
}
```

**Dependencies**:
- `IEmailNotificationService` - Core email sending service
- `IContributorNotifyTemplateFactory` - Template selection factory
- `IMapper` (AutoMapper) - Data transformation

**Dependency Injection**:
- Registered as **Singleton** in `StartupExtensions.cs` (line 66)

---

### 3. MessageDto Model

**File**: `Dfe.Academies.External.Web/Models/Notifications/MessageDto.cs`

**Purpose**: Data transfer object representing an email message to be sent via GOV.UK Notify.

**Properties**:
| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `EmailAddress` | `string` | ✅ Yes | Recipient's email address |
| `TemplateId` | `string` | ✅ Yes | GOV.UK Notify template ID (GUID format) |
| `Personalisation` | `Dictionary<string, dynamic>?` | ❌ Optional | Key-value pairs for template variable substitution |
| `Reference` | `string?` | ❌ Optional | Unique reference identifier for tracking (no personal info) |
| `EmailReplyToId` | `string?` | ❌ Optional | Reply-to email template ID (GUID format) |

**Design Notes**:
- Uses template-based approach (templates defined in GOV.UK Notify dashboard)
- Template IDs are GUIDs stored in `appsettings.json`
- Personalisation dictionary maps template placeholders to values

---

### 4. Template Factory System

**Files**:
- `Factories/ContributorNotifyTemplateFactory.cs`
- `Factories/IContributorNotifyTemplateFactory.cs`
- `Models/EmailTemplates/IContributorTemplate.cs`
- `Models/EmailTemplates/FormAMatChairContributor.cs`
- `Models/EmailTemplates/FormAMatNonChairContributor.cs`
- `Models/EmailTemplates/JoinAMatChairContributor.cs`
- `Models/EmailTemplates/JoinAMatNonChairContributor.cs`

**Purpose**: Factory pattern to select appropriate GOV.UK Notify template based on business rules.

**How It Works**:
1. Four concrete template implementations registered in DI container:
   - `FormAMatChairContributor` → Uses `FamChairTemplateId`
   - `FormAMatNonChairContributor` → Uses `FamNonChairTemplateId`
   - `JoinAMatChairContributor` → Uses `JamChairTemplateId`
   - `JoinAMatNonChairContributor` → Uses `JamNonChairTemplateId`

2. Factory receives all implementations via constructor injection
3. `Get(applicationType, schoolRole)` method filters to find matching template

**Template Selection Logic**:
```csharp
public IContributorTemplate Get(ApplicationTypes applicationType, SchoolRoles schoolRole)
{
    return _emailTemplates.First(x =>
        x.ApplicationType == applicationType && x.SchoolRole == schoolRole);
}
```

**Dependency Injection**:
- All `IContributorTemplate` implementations registered as **Singleton** in `Program.cs` (lines 191-194)
- Factory registered as **Singleton** in `Program.cs` (line 195)

---

### 5. Dependency Injection Configuration

**File**: `Dfe.Academies.External.Web/Program.cs`

**Key Registrations** (lines 189-196):

```csharp
// GOV.UK Notify client - registered as Transient
builder.Services.AddTransient<IAsyncNotificationClient, NotificationClient>(
    x => new NotificationClient(builder.Configuration["emailnotifications:key"]));

// Template settings from appsettings.json
builder.Services.Configure<NotifyTemplateSettings>(
    builder.Configuration.GetSection("govuk-notify"));

// Contributor template implementations - all Singletons
builder.Services.AddSingleton<IContributorTemplate, FormAMatChairContributor>(...);
builder.Services.AddSingleton<IContributorTemplate, FormAMatNonChairContributor>(...);
builder.Services.AddSingleton<IContributorTemplate, JoinAMatChairContributor>(...);
builder.Services.AddSingleton<IContributorTemplate, JoinAMatNonChairContributor>(...);

// Factory and service - both Singletons
builder.Services.AddSingleton<IContributorNotifyTemplateFactory, ContributorNotifyTemplateFactory>();
builder.Services.AddSingleton<IEmailNotificationService, EmailNotificationService>();
```

**Registration Notes**:
- `IAsyncNotificationClient` is **Transient** (new instance per dependency resolution)
- `IEmailNotificationService` is **Singleton** (one instance for application lifetime)
- All template implementations are **Singleton**
- Factory is **Singleton**

**Considerations**:
- ⚠️ `IAsyncNotificationClient` as Transient may be inefficient if GOV.UK Notify client maintains connections
- ⚠️ Singleton services should be thread-safe (GOV.UK Notify client should handle this)

---

## Email Trigger Points

### 1. Contributor Invitation Email

**Location**: `Pages/AddAContributor.cshtml.cs`

**Trigger**: User adds a new contributor to an application (POST request)

**Flow**:
1. User fills form with contributor details (name, email, role)
2. Validation passes
3. Contributor added to application via `_academisationCreationService.AddContributorToApplication()`
4. Email sent via `_contributorEmailSenderService.SendInvitationToContributor()`
5. Page displays confirmation banner

**Code Reference** (lines 138-139):
```csharp
await _contributorEmailSenderService.SendInvitationToContributor(
    draftConversionApplication.ApplicationType, 
    ContributorRole,
    EmailAddress!, 
    emailVariables);
```

**Template Selection**: Based on `ApplicationType` and `ContributorRole` (ChairOfGovernors vs. other roles)

**Business Context**: Invites school staff members to collaborate on an academy conversion application

---

### 2. Problem Report Email

**Location**: `Pages/Help/Problem.cshtml.cs`

**Trigger**: User reports a problem with the form (POST request)

**Flow**:
1. User selects whether they want to be contacted
2. User provides problem description (and email if contact requested)
3. Validation passes
4. Email sent to support team via `emailNotificationService.SendAsync()`
5. User redirected to "ThankYou" page

**Code Reference** (lines 71-90):
```csharp
var personalization = new Dictionary<string, object>();
if (DoYouWantToBeContacted == SelectOption.Yes)
{
    personalization.Add("what_problem_did_you_notice_response", ProblemSummary);
    personalization.Add("problem_email_address", EmailAddress);
    this.templateId = this.notifyTemplateSettings.ProblemWithTheFormResponseNeededTemplateId;
}
else
{
    personalization.Add("what_problem_did_you_notice", ProblemSummary);
    this.templateId = this.notifyTemplateSettings.ProblemWithTheFormNoResponseNeededTemplateId;
}

var message = new MessageDto(this.configuration["emailnotifications:supportemail"], this.templateId)
{
    Personalisation = personalization,
};
await this.emailNotificationService.SendAsync(message);
```

**Template Selection**: Two templates:
- `ProblemWithTheFormResponseNeededTemplateId` - If user wants contact
- `ProblemWithTheFormNoResponseNeededTemplateId` - If no contact needed

**Recipient**: Support email from configuration (`emailnotifications:supportemail`)

---

### 3. Feedback Submission Email

**Location**: `Pages/Help/Feedback.cshtml.cs`

**Trigger**: User submits feedback about the service (POST request)

**Flow**:
1. User selects feedback sentiment (How_do_you_feel)
2. User provides improvement suggestions
3. Validation passes
4. Email sent to support team via `emailNotificationService.SendAsync()`
5. User redirected to "ThankYou" page

**Code Reference** (lines 51-61):
```csharp
var personalization = new Dictionary<string, object>();
personalization.Add("How_do_you_feel", Feedback.GetDescription());
personalization.Add("what_improvements", FeedbackSummary);

var message = new MessageDto(this.configuration["emailnotifications:supportemail"], this.templateId)
{
    Personalisation = personalization,
};
await this.emailNotificationService.SendAsync(message);
```

**Template**: `FeedbackTemplateId` (single template)

**Recipient**: Support email from configuration

---

### 4. Application Help Request Email

**Location**: `Pages/Help/ApplicationHelp.cshtml.cs`

**Trigger**: User requests help with a specific application (POST request)

**Flow**:
1. User selects application from their pending applications list
2. User provides help description and contact email
3. Validation passes
4. Email sent to support team via `emailNotificationService.SendAsync()`
5. User redirected to "ThankYou" page

**Code Reference** (lines 69-80):
```csharp
var personalization = new Dictionary<string, object>();
personalization.Add("what_do_you_need_help_with", HelpSummary);
personalization.Add("help_email_address", EmailAddress);
personalization.Add("app_ref", SelectedReferenceNumber);

var message = new MessageDto(this.configuration["emailnotifications:supportemail"], this.templateId)
{
    Personalisation = personalization,
};
await this.emailNotificationService.SendAsync(message);
```

**Template**: `HelpWithAnApplicationTemplateId`

**Recipient**: Support email from configuration

---

### 5. Application Deletion Notification Email (Batch)

**Location**: `Pages/DeleteApplication.cshtml.cs`

**Trigger**: User deletes an application (POST request)

**Flow**:
1. User confirms application deletion
2. Permission check passes
3. Application cancelled via `_academisationService.CancelApplication(appId)`
4. **Loop through all contributors** to send notification emails
5. User redirected to "YourApplications" page

**Code Reference** (lines 109-123):
```csharp
foreach (Dtos.ConversionApplicationContributor i in contibutors)
{
    var personalization = new Dictionary<string, object>();
    personalization.Add("app_ref", draftConversionApplication.ApplicationReference);
    personalization.Add("application_canceller", leadContributor);
    personalization.Add("app_type", applicationType);

    var message = new MessageDto(i.EmailAddress, this.templateId)
    {
        Personalisation = personalization,
    };

    await _emailNotificationService.SendAsync(message);
}
```

**Template**: `ApplicationDeletedId` (single template for all contributors)

**Recipients**: All contributors associated with the application (batch sending)

**Special Note**: This is the only email trigger that sends to multiple recipients (one email per contributor in a loop)

---

## Configuration

### Required Settings in `appsettings.json`

#### Email Notifications Configuration

```json
"emailnotifications": {
    "key": "your-api-key-here",
    "SupportEmail": "support@example.gov.uk",
    "TestMode": true
}
```

**Settings**:
- `key` (required): API key from GOV.UK Notify service account
- `SupportEmail` (optional): Email address for support/help emails
- `TestMode` (required): Boolean flag to enable test mode filtering

**Test Mode Behavior**:
- When `true`: Only sends emails to addresses ending with `@education.gov.uk`
- Emails to other addresses are silently dropped (no error, no logging)
- Used for preventing accidental emails to real users during testing

#### GOV.UK Notify Template IDs

```json
"govuk-notify": {
    "JamChairTemplateId": "00000000-0000-0000-0000-000000000001",
    "JamNonChairTemplateId": "00000000-0000-0000-0000-000000000002",
    "FamChairTemplateId": "00000000-0000-0000-0000-000000000003",
    "FamNonChairTemplateId": "00000000-0000-0000-0000-000000000004",
    "HelpWithAnApplicationTemplateId": "00000000-0000-0000-0000-000000000005",
    "ProblemWithTheFormResponseNeededTemplateId": "00000000-0000-0000-0000-000000000006",
    "ProblemWithTheFormNoResponseNeededTemplateId": "00000000-0000-0000-0000-000000000007",
    "FeedbackTemplateId": "00000000-0000-0000-0000-000000000008",
    "ApplicationDeletedId": "00000000-0000-0000-0000-000000000009"
}
```

**Template IDs**: All are GUIDs that correspond to templates created in GOV.UK Notify dashboard.

**Template Mapping**:
- `JamChairTemplateId` → JoinAMat Chair of Governors invitation
- `JamNonChairTemplateId` → JoinAMat Non-Chair contributor invitation
- `FamChairTemplateId` → FormAMat Chair of Governors invitation
- `FamNonChairTemplateId` → FormAMat Non-Chair contributor invitation
- `HelpWithAnApplicationTemplateId` → Application help request
- `ProblemWithTheFormResponseNeededTemplateId` → Problem report (contact requested)
- `ProblemWithTheFormNoResponseNeededTemplateId` → Problem report (no contact)
- `FeedbackTemplateId` → Feedback submission
- `ApplicationDeletedId` → Application deletion notification

---

## Identified Gaps and Issues

### 1. Exception Handling ❌

**Issue**: No try-catch block around `SendEmailAsync` call in `EmailNotificationService.SendAsync()`.

**Impact**:
- If GOV.UK Notify API fails, exception bubbles up to middleware
- User sees generic error page instead of graceful handling
- No logging of email sending failures
- User experience degraded

**Code Location**: `Services/EmailNotificationService.cs` line 42

**Current Behavior**: Exceptions handled by `BespokeExceptionHandlingMiddleware` which redirects to `/Error` page.

---

### 2. No Queuing/Background Processing ⚠️

**Issue**: All emails sent synchronously within HTTP request pipeline.

**Impact**:
- User waits for email API call to complete before seeing response
- If GOV.UK Notify API is slow, entire request is delayed
- No resilience if GOV.UK Notify service is temporarily unavailable
- Cannot batch or rate-limit emails
- Request timeout risk if API is very slow

**Current Flow**: Request → Send Email → Wait for Response → Return to User

**Alternative**: Request → Queue Email → Return Immediately → Background Worker Sends Email

---

### 3. No Retry Logic ❌

**Issue**: If GOV.UK Notify API call fails (network error, temporary service outage), no automatic retry.

**Impact**:
- Temporary failures result in permanent email loss
- Network blips cause email delivery failures
- No exponential backoff or circuit breaker pattern

**Scenarios Not Handled**:
- Transient network errors
- GOV.UK Notify rate limiting (429 responses)
- Service degradation (500 errors)

---

### 4. Limited Error Logging ⚠️

**Issue**: Only success is logged. Failures are not logged before exception propagation.

**Current Logging**:
```csharp
_logger.LogInformation($"Email successfully Sent to:- {message.EmailAddress}");
```

**Missing**:
- Failure logging with exception details
- API response code logging
- Email content/metadata logging for debugging
- Correlation IDs for tracking

---

### 5. Test Mode Silent Failure ⚠️

**Issue**: Test mode silently drops emails without logging.

**Code**:
```csharp
if (this.TestMode && !message.EmailAddress.ToLower().EndsWith("@education.gov.uk")) {
    return; // Silent return - no logging
}
```

**Impact**:
- Developers may not realize emails are being blocked
- No audit trail of blocked emails
- Difficult to debug why emails aren't sending in test environment

---

### 6. No Email Delivery Tracking ❌

**Issue**: No mechanism to track email delivery status or handle bounces/failures.

**Impact**:
- Cannot determine if email was successfully delivered
- No notification if email bounces
- Cannot resend failed emails
- No audit trail of email history

**GOV.UK Notify Capabilities** (not utilized):
- Delivery receipts
- Bounce handling
- Status callbacks
- Email history API

---

### 7. Synchronous Batch Sending (DeleteApplication) ⚠️

**Issue**: `DeleteApplication.cshtml.cs` sends emails in a synchronous loop.

**Code**:
```csharp
foreach (Dtos.ConversionApplicationContributor i in contibutors)
{
    await _emailNotificationService.SendAsync(message);
}
```

**Impact**:
- If there are many contributors, user waits for all emails
- If one email fails, subsequent emails may not be sent (if exception propagates)
- No parallelization or rate limiting

---

## Verification Checklist

### Configuration Verification

- [ ] Verify `emailnotifications:key` is set and valid in all environments
- [ ] Verify `emailnotifications:testmode` is correctly set per environment
  - [ ] Production: `false`
  - [ ] Test/Dev: `true` or `false` as appropriate
- [ ] Verify `emailnotifications:supportemail` is set and monitored
- [ ] Verify all template IDs in `govuk-notify` section match GOV.UK Notify dashboard
- [ ] Verify template IDs are valid GUIDs

### Test Mode Verification

- [ ] Test with `TestMode: true` and `@education.gov.uk` address → Email should send
- [ ] Test with `TestMode: true` and non-`@education.gov.uk` address → Email should be silently dropped
- [ ] Verify test mode behavior is logged or documented for team awareness
- [ ] Test with `TestMode: false` → All valid addresses should receive emails

### Exception Scenario Testing

- [ ] Test with invalid API key → Should handle gracefully (currently will show error page)
- [ ] Test with invalid template ID → Should handle gracefully
- [ ] Test with invalid email address format → Should handle gracefully
- [ ] Test with network failure → Should handle gracefully
- [ ] Test with GOV.UK Notify service outage → Should handle gracefully

### Template Validation

- [ ] Verify all 9 templates exist in GOV.UK Notify dashboard
- [ ] Verify template personalisation variables match code usage:
  - [ ] Contributor templates use: `ContributorName`, `InvitingUsername`, `SchoolName`
  - [ ] Problem template (response): `what_problem_did_you_notice_response`, `problem_email_address`
  - [ ] Problem template (no response): `what_problem_did_you_notice`
  - [ ] Feedback template: `How_do_you_feel`, `what_improvements`
  - [ ] Application help: `what_do_you_need_help_with`, `help_email_address`, `app_ref`
  - [ ] Application deleted: `app_ref`, `application_canceller`, `app_type`

### Integration Testing

- [ ] Test contributor invitation email flow end-to-end
- [ ] Test all help/feedback email flows
- [ ] Test application deletion email (with multiple contributors)
- [ ] Verify emails arrive in recipient inboxes
- [ ] Verify email content matches template expectations
- [ ] Test email sending during high load scenarios

### Monitoring and Observability

- [ ] Verify Application Insights captures email sending telemetry
- [ ] Verify email success logs are visible in logs
- [ ] Verify failure scenarios are logged appropriately
- [ ] Set up alerts for email sending failures (if monitoring available)

### Security Verification

- [ ] Verify API key is stored securely (not in source control)
- [ ] Verify API key has appropriate permissions in GOV.UK Notify
- [ ] Verify email addresses are validated before sending
- [ ] Verify no personal data is logged inappropriately

---

## Summary

### Strengths ✅

1. Uses modern async/await patterns
2. Template-based system (no hardcoded content)
3. Factory pattern for maintainable template selection
4. GOV.UK Notify is a reliable, government-approved service
5. Test mode prevents accidental emails

### Weaknesses ❌

1. No exception handling around email sending
2. No queuing/background processing
3. No retry logic for transient failures
4. Limited error logging
5. Test mode silent failures
6. Synchronous batch sending in DeleteApplication

### Recommendations Priority

See `EMAIL_RECOMMENDATIONS.md` for detailed improvement recommendations.

**High Priority**:
- Add exception handling and error logging
- Implement retry logic with exponential backoff

**Medium Priority**:
- Add background processing/queuing
- Improve test mode logging

**Low Priority**:
- Add email delivery tracking
- Optimize batch sending

