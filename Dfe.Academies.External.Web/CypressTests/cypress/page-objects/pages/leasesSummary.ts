class LeasesSummary {
  public leasesSummaryElementsVisible(): this {
    cy.leasesSummaryElementsVisible()

    return this
  }

  public leasesSelectOptionNo(): this {
    cy.get('#anyLeasesOptionNo').click()
    cy.get('#anyLeasesOptionNo').should('be.checked')

    return this
  }

  public submitLeasesSummary(): this {
    cy.submitLeasesSummary()

    return this
  }
}

const leasesSummary = new LeasesSummary()

export default leasesSummary
