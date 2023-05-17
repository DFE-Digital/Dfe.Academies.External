import BasePage from "../BasePage"
export default class A2BCurrentFinancialYear extends BasePage {


static currentFinancialYrElementsVisible()
{
    cy.currentFinancialYrElementsVisible()
}

static inputCurrentFinancialYrDate()
{
    cy.get('#sip_cfyenddate-day').type('31')
    cy.get('#sip_cfyenddate-month').type('03')
    cy.get('#sip_cfyenddate-year').type('2023')
}

static inputCurrentFinancialYrRevenueCarryForward()
{
    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('99999.99')
}

static selectRevenueCarryForwardDeficit()
{
    cy.get('#revenueTypeDeficit').click()
}

static verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays()
{
    cy.verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays()
}

static inputReasonsForCurrentRevenueCarryForwardDeficit()
{
    var reasonsRevenueCarryForwardDeficit
    reasonsRevenueCarryForwardDeficit = 'A) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('label[for="CFYRevenueCarryForwardExplained"]').type(reasonsRevenueCarryForwardDeficit)
}

static uploadFileForCurrentRevenueCarryForwardDeficit()
{
    cy.uploadFileForCurrentRevenueCarryForwardDeficit()
}

static inputCurrentFinancialYrCapitalCarryForward()
{
    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('99998.98')
}

static selectCurrentCapitalCarryForwardDeficit()
{
    cy.selectCurrentCapitalCarryForwardDeficit()
}

static verifyCurrentCapitalCarryForwardDeficitSelectedSectionDisplays()
{
    cy.verifyCurrentCapitalCarryForwardDeficitSelectedSectionDisplays()
}

static inputReasonsForCurrentCapitalCarryForwardDeficit()
{   var reasonsCapitalCarryForwardDeficit
    reasonsCapitalCarryForwardDeficit = 'B) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author'
    cy.get('label[for="CFYCapitalCarryForwardExplained"]').type(reasonsCapitalCarryForwardDeficit)
}

static uploadFileForCurrentCapitalCarryForwardDeficit()
{
    cy.uploadFileForCurrentCapitalCarryForwardDeficit()
}

static submitCurrentFinancialYr()
{
    cy.submitCurrentFinancialYr()
}

}