class PreviousFinancialYear {
  public previousFinancialYrElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Finances (Step 1 of 6)')
    cy.get('.govuk-heading-l').contains('Previous financial year')
    cy.get('legend').contains('End of previous financial year')
    cy.get('#pfy-end-date-hint').contains('For example, 01 09 2022')

    cy.get('label[for=sip_pfyenddate-day]').contains('Day')
    cy.get('#sip_pfyenddate-day').should('be.visible').should('be.enabled')

    cy.get('label[for=sip_pfyenddate-month]').contains('Month')
    cy.get('#sip_pfyenddate-month').should('be.visible').should('be.enabled')

    cy.get('label[for=sip_pfyenddate-year]').contains('Year')
    cy.get('#sip_pfyenddate-year').should('be.visible').should('be.enabled')

    cy.get('label[for=Revenue]').contains('Revenue carry forward at end of the previous financial year (31 March)')

    cy.get('#Revenue').should('be.visible').should('be.enabled')

    cy.get('#revenueRevenueTypeSurplus').should('not.be.checked')
    cy.get('label[for=revenueRevenueTypeSurplus]').contains('Surplus')

    cy.get('#revenueRevenueTypeDeficit').should('not.be.checked')
    cy.get('label[for=revenueRevenueTypeDeficit]').contains('Deficit')

    cy.get('label[for=CapitalCarryForward]').contains('Capital carry forward at end of the previous financial year (31 March)')

    cy.get('#CapitalCarryForward').should('be.visible').should('be.enabled')

    cy.get('#capitalRevenueTypeSurplus').should('not.be.checked')
    cy.get('label[for=capitalRevenueTypeSurplus]').contains('Surplus')

    cy.get('#capitalRevenueTypeDeficit').should('not.be.checked')
    cy.get('label[for=capitalRevenueTypeDeficit]').contains('Deficit')

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

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
