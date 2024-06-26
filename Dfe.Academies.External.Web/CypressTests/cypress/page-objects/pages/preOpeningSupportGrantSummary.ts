class PreOpeningSupportGrantSummary {
  public selectPreopeningSupportGrantStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public preopeningSupportGrantSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Pre-opening support grant')

    cy.get('.govuk-link').contains('Change your answers')

    cy.get('b').eq(0).contains('Do you want these funds paid to the school or the trust the school is joining?')
    // cy.get('p').eq(2).contains('To the school')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  public submitPreopeningSupportGrantSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const preOpeningSupportGrantSummary = new PreOpeningSupportGrantSummary()

export default preOpeningSupportGrantSummary
