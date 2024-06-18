class WhatAreYouApplyingToDo {
  public selectJAMRadioButtonVerifyAndSubmit(): this {
    cy.get('input[type=radio]').eq(0).click()
    cy.get('input[type=radio]').eq(0).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }

  public selectFAMRadioButtonVerifyAndSubmit(): this {
    cy.get('input[type=radio]').eq(1).click()
    cy.get('input[type=radio]').eq(1).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }
}

const whatAreYouApplyingToDo = new WhatAreYouApplyingToDo()

export default whatAreYouApplyingToDo
