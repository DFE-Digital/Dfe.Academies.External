import BasePage from "../BasePage"
export default class A2BYourApplication extends BasePage {


static financeSummaryNotStartedElementsVisible()
{
    cy.financeSummaryNotStartedElementsVisible()
}
static selectPreviousFinancialYrStartSection()
{
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()
}

static selectCurrentFinancialYrStartSection()
{
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(1).click()
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

static submitFinanceSummary()
{
    cy.submitFinanceSummary()
}

}