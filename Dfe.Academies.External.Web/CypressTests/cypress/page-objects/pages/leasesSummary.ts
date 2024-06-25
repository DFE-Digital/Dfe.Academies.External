class LeasesSummary {
  public enterLeasesDetails(): this {
    cy.get('#anyLeasesOptionNo').click()
    cy.get('#anyLeasesOptionNo').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const leasesSummary = new LeasesSummary()

export default leasesSummary
