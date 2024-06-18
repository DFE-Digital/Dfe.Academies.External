class PreopeningSupportGrantDetails {
  public selectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails(): this {
    cy.get('#pay-toSchool').click()

    cy.get('#pay-toSchool').should('be.checked')
    cy.get('.govuk-body').eq(1).should('be.visible').contains('Go to provide information about your banking payments to DfE to add the school’s bank details.')
    cy.get('#funds-paid-to-school-hint').should('be.visible').contains('Your application can be submitted without completing this action now, however please provide your bank details either before or shortly after submission of your application.')

    cy.get('input[type=submit]').click()

    return this
  }

  public FAMSelectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails(): this {
    cy.get('#ConfirmSchoolPay').click()
    cy.get('#ConfirmSchoolPay').should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }
}

const preopeningSupportGrantDetails = new PreopeningSupportGrantDetails()

export default preopeningSupportGrantDetails
