import 'cypress-file-upload'

class CurrentFinancialYear {
  // TODO get better selectors for elements
  // TODO make date input a parameter
  public enterCurrentFinancialYearDetails(): this {
    cy.get('#sip_cfyenddate-day').type('31')
    cy.get('#sip_cfyenddate-month').type('03')
    cy.get('#sip_cfyenddate-year').type('2023')

    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('99999.99')

    cy.get('#revenueTypeDeficit').click()
    cy.get('#revenueTypeDeficit').should('be.checked')

    cy.get('label[for=schoolCfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(1).contains('Uploaded files')

    cy.get('hr').eq(0).should('be.visible')
    cy.get('.govuk-label').eq(9).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')

    cy.get('label[for=CFYRevenueCarryForwardExplained]').type('Reason for the revenue deficit')

    cy.get('#schoolCfyRevenueFileUpload').attachFile('../fixtures/fifty-k.docx')

    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('99998.98')

    cy.get('#capitalTypeDeficit').click()

    cy.get('#capitalTypeDeficit').should('be.checked')

    cy.get('label[for=schoolCfyRevenueFileUpload]').contains('Upload a file')

    cy.get('legend').eq(2).contains('Uploaded files')

    cy.get('hr').eq(2).should('be.visible')
    cy.get('.govuk-label').eq(16).contains('No file uploaded')
    cy.get('hr').eq(3).should('be.visible')

    cy.get('label[for=CFYCapitalCarryForwardExplained]').type('Reason for the capital deficit')

    cy.get('#schoolCfyCapitalFileUpload').attachFile('../fixtures/fiftyk.pdf')

    cy.get('input[type=submit]').click()

    return this
  }
}

const currentFinancialYear = new CurrentFinancialYear()

export default currentFinancialYear
