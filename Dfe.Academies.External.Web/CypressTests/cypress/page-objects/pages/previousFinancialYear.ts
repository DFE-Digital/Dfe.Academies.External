class PreviousFinancialYear {
  public previousFinancialYrElementsVisible(): this {
    cy.previousFinancialYrElementsVisible()

    return this
  }

  public inputPreviousFinancialYrDataAndSubmit(): this {
    cy.get('#sip_pfyenddate-day').type('31')
    cy.get('#sip_pfyenddate-month').type('03')
    cy.get('#sip_pfyenddate-year').type('2022')

    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('4999.99')

    cy.get('#revenueRevenueTypeSurplus').click()

    cy.get('#revenueRevenueTypeSurplus').should('be.checked')

    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('4998.98')

    cy.get('#capitalRevenueTypeSurplus').click()

    cy.get('#capitalRevenueTypeSurplus').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const previousFinancialYear = new PreviousFinancialYear()

export default previousFinancialYear
