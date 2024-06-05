class DeclarationSummary {
  public declarationSummaryElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Declaration')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('I agree with all of these statements, and believe that the facts stated in this application are true')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  public declarationStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public declarationSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Declaration')

    cy.get('.govuk-link').contains('Change your answers')

    cy.get('b').eq(0).contains('I agree with all of these statements, and believe that the facts stated in this application are true')
    cy.get('p').eq(2).contains('Yes')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  public submitDeclarationSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const declarationSummary = new DeclarationSummary()

export default declarationSummary
