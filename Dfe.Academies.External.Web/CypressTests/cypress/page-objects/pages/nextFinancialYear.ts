class NextFinancialYear {
  public enterNextFinancialYearDetails(): this {
    const filepath = '../fixtures/fiftyk.pdf'

    cy.get('#sip_nfyenddate-day').type('31')
    cy.get('#sip_nfyenddate-month').type('03')
    cy.get('#sip_nfyenddate-year').type('2024')

    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('199999.99')

    cy.get('#revenueTypeDeficit').click()
    cy.get('#revenueTypeDeficit').should('be.checked')

    cy.get('label[for=schoolNfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(1).contains('Uploaded files')

    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').eq(9).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')

    cy.get('#NFYRevenueStatusExplained').type('Reason for the revenue carry deficit')

    cy.get('#schoolNfyRevenueFileUpload').attachFile(filepath)

    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('199998.98')

    cy.get('#capitalRevenueTypeDeficit').click()

    cy.get('#capitalRevenueTypeDeficit').should('be.checked')

    cy.get('label[for=schoolNfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(2).contains('Uploaded files')

    cy.get('hr').eq(2).should('be.visible')
    cy.get('.govuk-label').eq(16).contains('No file uploaded')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('#PFYCapitalCarryForwardExplained').type('Reason for the capital carry deficit')

    cy.get('#schoolNfyCapitalFileUpload').attachFile(filepath)

    cy.get('input[type=submit]').click()

    return this
  }
}

const nextFinancialYear = new NextFinancialYear()

export default nextFinancialYear
