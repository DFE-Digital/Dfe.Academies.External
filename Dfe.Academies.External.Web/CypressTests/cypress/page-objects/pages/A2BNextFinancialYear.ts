import BasePage from "../BasePage"
export default class A2BNextFinancialYear extends BasePage {


static nextFinancialYrElementsVisible()
{
    cy.nextFinancialYrElementsVisible()
}

static inputNextFinancialYrDataAndSubmit()
{
    cy.inputNextFinancialYrDate()

    cy.inputNextFinancialYrRevenueCarryForward()

    cy.get('#revenueTypeDeficit').click()

    cy.verifyNextRevenueCarryForwardDeficitSelectedSectionDisplays()

    cy.inputReasonsForNextRevenueCarryForwardDeficit()

    cy.uploadFileForNextRevenueCarryForwardDeficit()

    cy.inputNextFinancialYrCapitalCarryForward()

    cy.selectNextCapitalCarryForwardDeficit()

    cy.verifyNextCapitalCarryForwardDeficitSelectedSectionDisplays()

    cy.inputReasonsForNextCapitalCarryForwardDeficit()

    cy.uploadFileForNextCapitalCarryForwardDeficit()

    cy.submitNextFinancialYr()
}


}