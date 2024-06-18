class LeasesSummary {
  public leasesSelectOptionNo(): this {
    cy.get('#anyLeasesOptionNo').click()
    cy.get('#anyLeasesOptionNo').should('be.checked')

    return this
  }

  public submitLeasesSummary(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const leasesSummary = new LeasesSummary()

export default leasesSummary
