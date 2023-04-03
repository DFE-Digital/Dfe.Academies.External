import BasePage from "../BasePage"
export default class A2BCurrentFinancialYear extends BasePage {


static currentFinancialYrElementsVisible()
{
    cy.currentFinancialYrElementsVisible()
}

static inputCurrentFinancialYrDate()
{
    cy.inputCurrentFinancialYrDate()
}

static inputCurrentFinancialYrRevenueCarryForward()
{
    cy.inputCurrentFinancialYrRevenueCarryForward()
}

static selectRevenueCarryForwardDeficit()
{
    cy.selectRevenueCarryForwardDeficit()
}

static verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays()
{
    cy.verifyCurrentRevenueCarryForwardDeficitSelectedSectionDisplays()
}

static inputReasonsForCurrentRevenueCarryForwardDeficit()
{
    cy.inputReasonsForCurrentRevenueCarryForwardDeficit()
}

static uploadFileForCurrentRevenueCarryForwardDeficit()
{
    cy.uploadFileForCurrentRevenueCarryForwardDeficit()
}

static inputCurrentFinancialYrCapitalCarryForward()
{
    cy.inputCurrentFinancialYrCapitalCarryForward()
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
{
    cy.inputReasonsForCurrentCapitalCarryForwardDeficit()
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