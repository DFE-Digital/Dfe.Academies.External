class WhatAreYouApplyingToDo {
  public whatAreYouApplyingToDoElementsVisible(): this {
    cy.get('a[href="/your-applications"]').contains('Back')
    cy.get('h1').contains('What are you applying to do?')
    cy.get('legend').contains('When a school becomes an academy, it must either join an existing trust or form a new one.')
    cy.get('input[type=radio]').eq(0).should('exist')
    cy.get('label[for=ApplicationTypeJoinAMat]').contains('Join a multi-academy trust')
    cy.get('input[type=radio]').eq(1).should('exist')
    cy.get('label[for=ApplicationTypeFormAMat]').contains('Form a new multi-academy trust')
    cy.get('.govuk-inset-text').should('be.visible').contains('If your school is unable to either join an existing trust or form one with other schools, you should contact your Regional Director')
    cy.get('.govuk-link').should('be.visible').contains('your Regional Director')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

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
