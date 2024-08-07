class ReasonsForFormingTrustSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public checkReasonsForFormingTrustSummaryCompleted(): this {
    cy.get('[data-cy="response"]').eq(0).contains('Why are the schools forming the trust?')
    cy.get('[data-cy="response"]').eq(1).contains('What vision and aspiration have the schools agreed for the trust?')
    cy.get('[data-cy="response"]').eq(2).contains('What geographical areas and communities will the trust service?')
    cy.get('[data-cy="response"]').eq(3).contains('How will the schools make the most of the freedoms that academies have?')
    cy.get('[data-cy="response"]').eq(4).contains('How will the schools work together to improve teaching and learning in the trust and potentially support other schools beyond the trust?')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const reasonsForFormingTrustSummary = new ReasonsForFormingTrustSummary()

export default reasonsForFormingTrustSummary
