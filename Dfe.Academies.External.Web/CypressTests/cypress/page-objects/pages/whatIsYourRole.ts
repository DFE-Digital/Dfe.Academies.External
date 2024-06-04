class WhatIsYourRole {
  public whatIsYourRoleElementsVisible(): this {
    cy.whatIsYourRoleElementsVisible()

    return this
  }

  public selectChairOfGovernorsRadioButtonVerifyAndSubmit(): this {
    cy.get('input[type=radio]').eq(0).click()
    cy.get('input[type=radio]').eq(0).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }

  public selectSomethingElseRadioButton(): this {
    cy.selectSomethingElseRadioButton()

    return this
  }

  public verifySomethingElseRadioButtonChecked(): this {
    cy.verifySomethingElseRadioButtonChecked()

    return this
  }
}

const whatIsYourRole = new WhatIsYourRole()

export default whatIsYourRole
