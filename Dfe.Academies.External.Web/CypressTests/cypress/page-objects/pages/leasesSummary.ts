class LeasesSummary {
  public leasesSummaryElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Finances (Step 5 of 6)')
    cy.get('.govuk-heading-l').contains('Leases')

    cy.get('legend').contains('Are there any existing leases?')

    cy.get('#anyLeasesOptionYes').should('not.be.checked')
    cy.get('label[for=anyLeasesOptionYes]').contains('Yes')

    cy.get('#anyLeasesOptionNo').should('not.be.checked')
    cy.get('label[for=anyLeasesOptionNo]').contains('No')

    cy.get('input[type=submit]').should('be.visible').contains('Continue')

    return this
  }

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
