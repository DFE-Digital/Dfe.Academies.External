class Declaration {
  public declarationElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Declaration')

    cy.get('.govuk-inset-text').contains('This section must be completed by the chair of the school\'s governing body. You can invite the chair to contribute if this is not you.')

    cy.get('p').eq(2).contains('As the chair of the governing body of the applying school, I confirm that the governing body agrees with these statements:')

    cy.get('li').eq(0).contains('The governing body has passed a resolution that the school should become an academy.')
    cy.get('li').eq(1).contains('The school will complete a consultation with relevant stakeholders (such as parents, staff, the local communities and others), and consider their equality needs before they sign the funding agreement.')
    cy.get('li').eq(2).contains('The school agrees to the terms set out in the academy pre-opening support grant certificate.')
    cy.get('li').eq(3).contains('The school agrees to provide any further information that the Department for Education needs to assess this application.')
    cy.get('li').eq(4).contains('That if any information in this application is false or misleading, this application may be rejected or the academy order may be revoked if it has already been awarded.')

    cy.get('.govuk-fieldset__heading').contains('I confirm that:')

    cy.get('#SchoolDeclarationTeacherChair').should('not.be.checked')
    cy.get('label[for=SchoolDeclarationTeacherChair]').contains('I am the chair of governors of the applying school')

    cy.get('#SchoolDeclarationBodyAgree').should('not.be.checked')
    cy.get('label[for=SchoolDeclarationBodyAgree]').contains('The information in this application is true to the best of my knowledge')

    cy.get('input[type=submit]').should('be.visible').contains('Save and return')

    return this
  }

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
