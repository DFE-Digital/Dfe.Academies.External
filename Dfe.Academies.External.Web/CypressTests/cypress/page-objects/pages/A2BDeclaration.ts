import BasePage from "../BasePage"
export default class A2BDeclaration extends BasePage {

static declarationElementsVisible()
{
    cy.declarationElementsVisible()
}

static selectAgreements()
{
    cy.selectAgreements()
}

static verifyAgreementsSelected()
{
    cy.verifyAgreementsSelected()
}

static submitDeclaration()
{
    cy.submitDeclaration()
}

}