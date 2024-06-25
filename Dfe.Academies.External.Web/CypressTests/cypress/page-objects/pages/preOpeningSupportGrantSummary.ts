class PreOpeningSupportGrantSummary {
  public startPreopeningSupportGrant(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO fix commented lines
  // TODO all of these elements require proper Cypress tags
  public checkPreopeningSupportGrantSummaryCompleted(): this {
    // cy.get('p').eq(2).contains('To the school')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const preOpeningSupportGrantSummary = new PreOpeningSupportGrantSummary()

export default preOpeningSupportGrantSummary
