class AboutTheConversion {
  public aboutTheConversionNotStartedElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains('You have not added any information')

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains('You have not added any information')

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains('You have not added any information')

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains('You have not added any information')

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('You have not added any information')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains('You have not added any information')

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(3).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

    return this
  }

  public selectContactDetailsStartSection(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()

    return this
  }

  public aboutTheConversionMainContactsCompleteElementsVisible(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('a[class=govuk-link]').eq(1).contains('Change your answers')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains(headTeacherName)

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains(headTeacherEmail)

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains(chairName)

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains(chairEmail)

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('The chair of the governing body')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains(approverName)

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains(approverEmail)

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')
    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('You have not added any information')

    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(2).should('be.visible').contains('Start section')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-button').should('be.visible').contains('Back to application overview')

    return this
  }

  public selectDateForConversionStartSection(): this {
    cy.contains('Start section').eq(0).click()

    return this
  }

  public aboutTheConversionCompleteElementsVisible(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('About the conversion')
    cy.get('.govuk-heading-m').eq(0).contains('Contact details')

    cy.get('a[class=govuk-link]').eq(1).contains('Change your answers')

    cy.get('.govuk-body').eq(0).contains('Name of headteacher')
    cy.get('.govuk-body').eq(1).contains(headTeacherName)

    cy.get('.govuk-body').eq(2).contains('Headteacher\'s email address')
    cy.get('.govuk-body').eq(3).contains(headTeacherEmail)

    cy.get('.govuk-body').eq(4).contains('Name of the chair of the governing body')
    cy.get('.govuk-body').eq(5).contains(chairName)

    cy.get('.govuk-body').eq(6).contains('Chair\'s email address')
    cy.get('.govuk-body').eq(7).contains(chairEmail)

    cy.get('.govuk-body').eq(8).contains('Who is the main contact for the conversion')
    cy.get('.govuk-body').eq(9).contains('The chair of the governing body')

    cy.get('.govuk-body').eq(10).contains('Approver\'s full name')
    cy.get('.govuk-body').eq(11).contains(approverName)

    cy.get('.govuk-body').eq(12).contains('Approver\'s email address')
    cy.get('.govuk-body').eq(13).contains(approverEmail)

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(1).contains('Date for conversion')
    cy.get('a[class=govuk-link]').eq(2).contains('Change your answers')

    cy.get('.govuk-body').eq(14).contains('Do you want the conversion to happen on a particular date?')
    cy.get('.govuk-body').eq(15).contains('No')

    // cy.get('hr').eq(9).should('be.visible')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(2).contains('Reasons for joining')
    cy.get('a[class=govuk-link]').eq(3).contains('Change your answers')
    cy.get('.govuk-body').eq(16).contains('Why does the school want to join this trust in particular?')
    cy.get('.govuk-body').eq(17).contains('Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.')

    // cy.get('hr').eq(10).should('be.visible')

    // HR PART OF START SECTION COMPONENT SECTION

    cy.get('.govuk-heading-m').eq(3).contains('Changing the name of the school')

    cy.get('a[class=govuk-link]').eq(4).contains('Change your answers')

    cy.get('.govuk-body').eq(18).contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('.govuk-body').eq(19).contains('No')

    return this
  }

  public submitAboutTheConversion(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const aboutTheConversion = new AboutTheConversion()

export default aboutTheConversion
