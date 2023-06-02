import BasePage from "../BasePage"
export default class A2BPreOpeningSupportGrantSummary extends BasePage {
    
static preopeningSupportGrantSummaryElementsVisible()
{
    cy.preopeningSupportGrantSummaryElementsVisible()    
}

static selectPreopeningSupportGrantStartSection()
{
    cy.contains('Start section').click()
}

static preopeningSupportGrantSummaryCompleteElementsVisible()
{
    cy.preopeningSupportGrantSummaryCompleteElementsVisible()
}

static submitPreopeningSupportGrantSummary()
{
    cy.get('.govuk-button').click()
}

}