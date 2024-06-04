import BasePage from ../basePage
export default class A2BFinancialInvestigations extends BasePage {


static financialInvestigationsElementsVisible()
{
    cy.financialInvestigationsElementsVisible()
}

static selectFinancialInvestigationsOptionNo()
{
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')
}

static submitFinancialInvestigations()
{
    cy.submitFinancialInvestigations()
}

}