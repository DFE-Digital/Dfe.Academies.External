class Declaration {
  public selectAgreementsVerifyAndSubmit(): this {
    cy.get('#SchoolDeclarationTeacherChair').click()
    cy.get('#SchoolDeclarationBodyAgree').click()

    cy.get('#SchoolDeclarationTeacherChair').should('be.checked')
    cy.get('#SchoolDeclarationBodyAgree').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const declaration = new Declaration()

export default declaration
