class WhatAreYouApplyingToDo {
  public startApplication(applicationType: string): this {
    applicationType = applicationType === 'Form A MAT' ? 'FormAMat' : 'JoinAMat'
    cy.get(`[id=ApplicationType${applicationType}]`).click()
    cy.get(`[id=ApplicationType${applicationType}]`).should('be.checked')
    cy.get('input[type=submit]').click()
    return this
  }
}

const whatAreYouApplyingToDo = new WhatAreYouApplyingToDo()

export default whatAreYouApplyingToDo
