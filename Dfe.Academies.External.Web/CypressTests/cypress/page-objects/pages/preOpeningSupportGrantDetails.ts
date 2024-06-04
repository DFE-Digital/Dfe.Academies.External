class PreopeningSupportGrantDetails {
  public preopeningSupportGrantDetailsElementsVisible(): this {
    cy.preopeningSupportGrantDetailsElementsVisible()

    return this
  }

  public selectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails(): this {
    cy.get('#pay-toSchool').click()
    cy.verifyToTheSchoolPreopeningSupportGrantDetailsSectionDisplays()
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
