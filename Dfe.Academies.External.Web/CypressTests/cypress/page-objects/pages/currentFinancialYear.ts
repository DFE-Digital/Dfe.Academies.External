import 'cypress-file-upload'

class CurrentFinancialYear {
  public inputCurrentFinancialYrDataAndSubmit(): this {
    cy.get('#sip_cfyenddate-day').type('31')
    cy.get('#sip_cfyenddate-month').type('03')
    cy.get('#sip_cfyenddate-year').type('2023')

    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('99999.99')

    cy.get('#revenueTypeDeficit').click()

    cy.get('#revenueTypeDeficit').should('be.checked')

    cy.get('label[for=CFYRevenueCarryForwardExplained]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(1).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#CFYRevenueCarryForwardExplained').should('be.visible').should('be.enabled')

    // cy.get('.govuk-label').eq(6).contains('You can upload the school\'s recovery plan.')

    cy.get('.govuk-hint').eq(2).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')

    cy.get('label[for=schoolCfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(1).contains('Uploaded files')

    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').eq(9).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')

    const reasonsRevenueCarryForwardDeficit = 'A) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('label[for=CFYRevenueCarryForwardExplained]').type(reasonsRevenueCarryForwardDeficit)

    const docxFilepath = '../fixtures/fifty-k.docx'
    cy.get('#schoolCfyRevenueFileUpload').attachFile(docxFilepath)

    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('99998.98')

    cy.get('#capitalTypeDeficit').click()

    cy.get('#capitalTypeDeficit').should('be.checked')

    cy.get('label[for=CFYCapitalCarryForwardExplained]').contains('Explain the reason for the deficit, how the school plan to deal with it, and the recovery plan.')
    cy.get('.govuk-hint').eq(3).contains('Provide details of the financial forecast and/or the deficit recovery plan agreed with the local authority')
    cy.get('#CFYCapitalCarryForwardExplained').should('be.visible').should('be.enabled')

    // cy.get('.govuk-label').eq(13).contains('You can upload the school\'s recovery plan.')

    cy.get('.govuk-hint').eq(4).contains('We prefer schools to set out their income and expenditure using the consistent financial reporting codes.')
    cy.get('a[href="https://www.gov.uk/guidance/consistent-financial-reporting-framework-cfr"]').contains('consistent financial reporting')

    cy.get('label[for=schoolCfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(2).contains('Uploaded files')

    cy.get('hr').eq(2).should('be.visible')
    cy.get('.govuk-label').eq(16).contains('No file uploaded')
    cy.get('hr').eq(3).should('be.visible')

    const reasonsCapitalCarryForwardDeficit = 'B) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('label[for=CFYCapitalCarryForwardExplained]').type(reasonsCapitalCarryForwardDeficit)

    const pdfFilepath = '../fixtures/fiftyk.pdf'
    cy.get('#schoolCfyCapitalFileUpload').attachFile(pdfFilepath)

    cy.get('input[type=submit]').click()

    return this
  }
}

const currentFinancialYear = new CurrentFinancialYear()

export default currentFinancialYear
