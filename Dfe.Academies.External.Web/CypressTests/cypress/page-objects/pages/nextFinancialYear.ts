class NextFinancialYear {
  public inputNextFinancialYrDataAndSubmit(): this {
    cy.get('#sip_nfyenddate-day').type('31')
    cy.get('#sip_nfyenddate-month').type('03')
    cy.get('#sip_nfyenddate-year').type('2024')

    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('199999.99')

    cy.get('#revenueTypeDeficit').click()

    cy.get('#revenueTypeDeficit').should('be.checked')

    cy.get('label[for=NFYRevenueStatusExplained]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(1).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#NFYRevenueStatusExplained').should('be.visible').should('be.enabled')

    // cy.get('.govuk-label').eq(6).contains('You can upload the school\'s recovery plan.')

    cy.get('.govuk-hint').eq(2).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')

    cy.get('label[for=schoolNfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(1).contains('Uploaded files')

    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').eq(9).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')

    const reasonsRevenueCarryForwardDeficit = 'C) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('#NFYRevenueStatusExplained').type(reasonsRevenueCarryForwardDeficit)

    const filepath = '../fixtures/fiftyk.pdf'
    cy.get('#schoolNfyRevenueFileUpload').attachFile(filepath)

    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('199998.98')

    cy.get('#capitalRevenueTypeDeficit').click()

    cy.get('#capitalRevenueTypeDeficit').should('be.checked')

    cy.get('label[for=PFYCapitalCarryForwardExplained]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(3).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#PFYCapitalCarryForwardExplained').should('be.visible').should('be.enabled')

    // cy.get('.govuk-label').eq(13).contains('You can upload the school\'s recovery plan.')

    cy.get('.govuk-hint').eq(4).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')

    cy.get('label[for=schoolNfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(2).contains('Uploaded files')

    cy.get('hr').eq(2).should('be.visible')
    cy.get('.govuk-label').eq(16).contains('No file uploaded')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('#PFYCapitalCarryForwardExplained').type('D) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author')

    cy.get('#schoolNfyCapitalFileUpload').attachFile(filepath)

    cy.get('input[type=submit]').click()

    return this
  }
}

const nextFinancialYear = new NextFinancialYear()

export default nextFinancialYear
