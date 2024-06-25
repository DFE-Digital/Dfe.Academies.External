class WhatAreYouApplyingToDo {
  public startApplication(applicationType: string): this {
    const radio = applicationType === 'Form A MAT' ? 1 : 0
    cy.get('input[type=radio]').eq(radio).click()
    cy.get('input[type=radio]').eq(radio).should('be.checked')
    cy.get('input[type=submit]').click()
    return this
  }
}

const whatAreYouApplyingToDo = new WhatAreYouApplyingToDo()

export default whatAreYouApplyingToDo
