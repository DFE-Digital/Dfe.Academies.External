import BasePage from ../basePage
export default class A2BLoansSummary extends BasePage {


static loansSummaryElementsVisible()
{
    cy.loansSummaryElementsVisible()
}

static selectLoansOptionNo()
{
    cy.get('#anyLoansOptionNo').click()
    cy.get('#anyLoansOptionNo').should('be.checked')

}

static submitLoansSummary()
{
    cy.submitLoansSummary()
}



}