class PreOpeningSupportGrantSummary {
  public startPreopeningSupportGrant(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public checkPreopeningSupportGrantSummaryCompleted(): this {
    cy.get('p').eq(2).contains('To the school')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const preOpeningSupportGrantSummary = new PreOpeningSupportGrantSummary()

export default preOpeningSupportGrantSummary
