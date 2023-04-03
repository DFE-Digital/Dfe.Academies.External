import BasePage from "../BasePage"
export default class A2BYourApplication extends BasePage {


static financeSummaryNotStartedElementsVisible()
{
    cy.financeSummaryNotStartedElementsVisible()
}
static selectPreviousFinancialYrStartSection()
{
    cy.selectPreviousFinancialYrStartSection()
}

static selectCurrentFinancialYrStartSection()
{
    cy.selectCurrentFinancialYrStartSection()
}

static selectNextFinancialYrStartSection()
{
    cy.selectNextFinancialYrStartSection()
}

static selectLoansStartSection()
{
    cy.selectLoansStartSection()
}

static selectLeasesStartSection()
{
    cy.selectLeasesStartSection()
}

static selectFinancialInvestigationsStartSection()
{
    cy.selectFinancialInvestigationsStartSection()
}

static financeSummaryCompleteElementsVisible()
{
    cy.financeSummaryCompleteElementsVisible()
}

}