import BasePage from "../BasePage"
export default class A2BDeclarationSummary extends BasePage {
    
static declarationSummaryElementsVisible()
{
    cy.declarationSummaryElementsVisible()    
}

static declarationStartSection()
{
    cy.contains('Start section').click()
}

static declarationSummaryCompleteElementsVisible()
{
    cy.declarationSummaryCompleteElementsVisible()
}

static submitDeclarationSummary()
{
    cy.get('.govuk-button').click()
}

}