class TrustDetailsSummary {
  // TODO get better selector for element
  public startTrustDetails(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get(`[data-cy="saveAndReturnButton"]`).eq(1).click()

    return this
  }
}

const trustDetailsSummary = new TrustDetailsSummary()

export default trustDetailsSummary
