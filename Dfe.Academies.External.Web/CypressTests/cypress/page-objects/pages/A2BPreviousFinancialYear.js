import BasePage from "../BasePage"
export default class A2BPreviousFinancialYear extends BasePage {


static previousFinancialYrElementsVisible()
{
    cy.previousFinancialYrElementsVisible()
}

static inputPreviousFinancialYrDate()
{
    cy.inputPreviousFinancialYrDate()
}

static inputPreviousFinancialYrRevenueCarryForward()
{
    cy.inputPreviousFinancialYrRevenueCarryForward()
}

static selectRevenueCarryForwardSurplus()
{
    cy.selectRevenueCarryForwardSurplus()
}

static verifyRevenueCarryForwardSurplusSelected()
{
    cy.verifyRevenueCarryForwardSurplusSelected()
}

static inputPreviousFinancialYrCapitalCarryForward()
{
    cy.inputPreviousFinancialYrCapitalCarryForward()
}

static selectCapitalCarryForwardSurplus()
{
    cy.selectCapitalCarryForwardSurplus()
}

static verifyCapitalCarryForwardSurplusSelected()
{
    cy.verifyCapitalCarryForwardSurplusSelected()
}

static submitPreviousFinancialYr()
{
    cy.submitPreviousFinancialYr()
}

}