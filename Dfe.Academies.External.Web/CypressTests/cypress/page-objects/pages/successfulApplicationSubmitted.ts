class SuccessfulApplicationSubmitted {
  public applicationSubmittedSuccessfullyElementsVisible(): this {
    cy.get('.govuk-panel__title').contains('Your application has been submitted')

    cy.get('.govuk-panel__body').contains('Your reference number is')

    // cy.get('strong').contains(`A2B_${applicationId}`)

    cy.get('.govuk-heading-m').eq(0).contains('Completed application')

    cy.get('.govuk-heading-m').eq(1).contains('What happens next')

    // cy.get('.govuk-body-m').eq(0).contains('It takes us 2 to 6 weeks to assess your application and grant your academy order, if you\'re successful.')

    cy.get('.govuk-body-m').eq(1).contains('Your project lead will contact you if they need to check anything.')

    cy.get('.govuk-body-m').eq(2).contains('These are the main steps in the conversion process:')

    cy.get('.sip-application-status--row-content').eq(0).contains('Application submission')

    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(1).contains('Regional schools commissioner makes a decision with advice from the Headteacher Board')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(2).contains('Academy order is issued')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(3).contains('School’s solicitor submits a land questionnaire, including site plan')

    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(4).contains('School’s solicitor submits draft funding agreement (and memorandum and articles of association for new trusts)')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(5).contains('School’s solicitor confirms that the commercial transfer agreement (CTA) and land arrangements are agreed')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(6).contains('School signs and submits the funding agreement')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(7).contains('School’s solicitor confirms TUPE and stakeholder consultation are complete')
    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(8).contains('School submits the academy bank details to ESFA')

    cy.get('div[class="sip-application-status--row-content-header govuk-body-m"]').eq(9).contains('Academy Opens')

    // cy.get('p').eq(4).contains('If have any queries about the progress of your application, please contact the Department for Education.')

    // cy.get('p').eq(5).contains('If you have any questions or comments about this service, e-mail regionalservices.rg@education.gov.uk')

    // cy.get('p').eq(6).contains('As we continue to develop this service we would value your feedback on your experience. Please complete our short survey. The survey takes around 10 minutes to complete')

    // cy.get('h2').eq(1).contains('If your application is successful')

    cy.contains('Prepare for conversion').should('be.visible')

    cy.get('hr').should('be.visible')

    return this
  }
}

const successfulApplicationSubmitted = new SuccessfulApplicationSubmitted()

export default successfulApplicationSubmitted
