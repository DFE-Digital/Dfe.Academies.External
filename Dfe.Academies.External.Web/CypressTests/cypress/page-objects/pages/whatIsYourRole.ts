class WhatIsYourRole {
  public whatIsYourRoleElementsVisible(): this {
    cy.get('h1').contains('What is your role?')
    cy.get('p').contains('Anyone from the school can contribute to this form, but only the chair of governors can complete the declaration section or submit it.')
    cy.get('input[type=radio]').eq(0).should('exist')
    cy.get('label[for=RoleTypeChairOfGovernors]').contains('The chair of the school\'s governors')
    cy.get('input[type=radio]').eq(1).should('exist')
    cy.get('label[for=RoleTypeOther]').contains('Something else')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public selectChairOfGovernorsRadioButtonVerifyAndSubmit(): this {
    cy.get('input[type=radio]').eq(0).click()
    cy.get('input[type=radio]').eq(0).should('be.checked')
    cy.get('input[type=submit]').click()

    return this
  }
}

const whatIsYourRole = new WhatIsYourRole()

export default whatIsYourRole
