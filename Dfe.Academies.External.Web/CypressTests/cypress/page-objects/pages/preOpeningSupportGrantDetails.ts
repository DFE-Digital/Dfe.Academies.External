class PreopeningSupportGrantDetails {
  public preopeningSupportGrantDetailsElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-heading-l').contains('Academy pre-opening support grant')

    cy.get('.govuk-body').eq(0).contains('If your application is successful you will be issued with an academy order. Once issued, the Department for Education will pay the pre-opening support grant into a nominated bank account.')

    cy.get('legend').contains('Do you want these funds paid to the school or the trust the school is joining?')

    cy.get('#pay-toSchool').should('not.be.checked')
    cy.get('label[for=pay-toSchool]').contains('To the school')

    cy.get('#pay-toTrust').should('not.be.checked')
    cy.get('label[for=pay-toTrust]').contains('To the trust')

    cy.get('input[type=submit]').should('be.visible').contains('Save and return to overview')

    return this
  }

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
