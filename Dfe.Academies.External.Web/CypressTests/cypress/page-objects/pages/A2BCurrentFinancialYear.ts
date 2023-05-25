import BasePage from "../BasePage"
export default class A2BCurrentFinancialYear extends BasePage {


static currentFinancialYrElementsVisible()
{
    cy.currentFinancialYrElementsVisible()
}

static inputCurrentFinancialYrDataAndSubmit()
{
    cy.get('#sip_cfyenddate-day').type('31')
    cy.get('#sip_cfyenddate-month').type('03')
    cy.get('#sip_cfyenddate-year').type('2023')

    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('99999.99')

    cy.get('#revenueTypeDeficit').click()

    cy.verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays()

    var reasonsRevenueCarryForwardDeficit
    reasonsRevenueCarryForwardDeficit = 'A) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('label[for="CFYRevenueCarryForwardExplained"]').type(reasonsRevenueCarryForwardDeficit)

    cy.uploadFileForCurrentRevenueCarryForwardDeficit()

    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('99998.98')

    cy.selectCurrentCapitalCarryForwardDeficit()

    cy.verifyCurrentCapitalCarryForwardDeficitSelectedSectionDisplays()
   var reasonsCapitalCarryForwardDeficit
    reasonsCapitalCarryForwardDeficit = 'B) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('label[for="CFYCapitalCarryForwardExplained"]').type(reasonsCapitalCarryForwardDeficit)

    cy.uploadFileForCurrentCapitalCarryForwardDeficit()

    cy.submitCurrentFinancialYr()
}

}