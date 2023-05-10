import BasePage from "../BasePage"
export default class A2BDeclarationSummary extends BasePage {
    
static declarationSummaryElementsVisible()
{
    cy.declarationSummaryElementsVisible()    
}

static declarationStartSection()
{
    cy.declarationStartSection()
}

static declarationSummaryCompleteElementsVisible()
{
    cy.declarationSummaryCompleteElementsVisible()
}

static submitDeclarationSummary()
{
    cy.submitDeclarationSummary()
}

}