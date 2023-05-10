import BasePage from "../BasePage"
export default class A2BLoansSummary extends BasePage {


static loansSummaryElementsVisible()
{
    cy.loansSummaryElementsVisible()
}

static submitLoansSummary()
{
    cy.submitLoansSummary()
}



}