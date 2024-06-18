class WhatIsYourRole {
  public selectChairOfGovernorsRadioButtonVerifyAndSubmit(): this {
    cy.get('input[type=radio]').eq(0).click()
    cy.get('input[type=radio]').eq(0).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }
}

const whatIsYourRole = new WhatIsYourRole()

export default whatIsYourRole
