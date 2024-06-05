class NextFinancialYear {
  public nextFinancialYrElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Finances (Step 3 of 6)')
    cy.get('.govuk-heading-l').contains('Next financial year')
    cy.get('legend').contains('End of next financial year')
    cy.get('#pfy-end-date-hint').contains('For example, 01 09 2022')

    cy.get('label[for=sip_nfyenddate-day]').contains('Day')
    cy.get('#sip_nfyenddate-day').should('be.visible').should('be.enabled')

    cy.get('label[for=sip_nfyenddate-month]').contains('Month')
    cy.get('#sip_nfyenddate-month').should('be.visible').should('be.enabled')

    cy.get('label[for=sip_nfyenddate-year]').contains('Year')
    cy.get('#sip_nfyenddate-year').should('be.visible').should('be.enabled')

    cy.get('label[for=Revenue]').contains('Forecasted revenue carry forward at end of the next financial year (31 March)')

    cy.get('#Revenue').should('be.visible').should('be.enabled')

    cy.get('#revenueTypeSurplus').should('not.be.checked')
    cy.get('label[for=revenueTypeSurplus]').contains('Surplus')

    cy.get('#revenueTypeDeficit').should('not.be.checked')
    cy.get('label[for=revenueTypeDeficit]').contains('Deficit')

    cy.get('label[for=CapitalCarryForward]').contains('Forecasted capital carry forward at end of the next financial year (31 March)')

    cy.get('#CapitalCarryForward').should('be.visible').should('be.enabled')

    cy.get('#capitalRevenueTypeSurplus').should('not.be.checked')
    cy.get('label[for=capitalRevenueTypeSurplus]').contains('Surplus')

    cy.get('#capitalRevenueTypeDeficit').should('not.be.checked')
    cy.get('label[for=capitalRevenueTypeDeficit]').contains('Deficit')

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

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

    cy.get('.govuk-label').eq(6).contains('You can upload the school\'s recovery plan.')

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

    cy.get('.govuk-label').eq(13).contains('You can upload the school\'s recovery plan.')

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
