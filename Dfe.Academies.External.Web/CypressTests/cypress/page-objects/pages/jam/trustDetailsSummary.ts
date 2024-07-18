class TrustDetailsSummary {
  public startTrustDetails(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get(`[data-cy="saveAndReturnButton"]`).click()

    return this
  }
}

const trustDetailsSummary = new TrustDetailsSummary()

export default trustDetailsSummary
