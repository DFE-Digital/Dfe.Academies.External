import BasePage from ../basePage
export default class A2BDeclaration extends BasePage {

static declarationElementsVisible()
{
    cy.declarationElementsVisible()
}

static selectAgreementsVerifyAndSubmit()
{
    cy.selectAgreements()
    cy.verifyAgreementsSelected()
    cy.get('input[type=submit]').click()
}

}