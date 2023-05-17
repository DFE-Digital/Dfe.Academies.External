import BasePage from "../BasePage"
export default class A2BPreviousFinancialYear extends BasePage {


static previousFinancialYrElementsVisible()
{
    cy.previousFinancialYrElementsVisible()
}

static inputPreviousFinancialYrDate()
{
    cy.get('#sip_pfyenddate-day').type('31')
    cy.get('#sip_pfyenddate-month').type('03')
    cy.get('#sip_pfyenddate-year').type('2022')
}

static inputPreviousFinancialYrRevenueCarryForward()
{
    cy.get('#Revenue').clear()
    cy.get('#Revenue').type('4999.99')
}

static selectRevenueCarryForwardSurplus()
{
    cy.get('#revenueRevenueTypeSurplus').click()
}

static verifyRevenueCarryForwardSurplusSelected()
{
    cy.get('#revenueRevenueTypeSurplus').should('be.checked')
}

static inputPreviousFinancialYrCapitalCarryForward()
{
    cy.get('#CapitalCarryForward').clear()
    cy.get('#CapitalCarryForward').type('4998.98')
}

static selectCapitalCarryForwardSurplus()
{
    cy.get('#capitalRevenueTypeSurplus').click()
}

static verifyCapitalCarryForwardSurplusSelected()
{
    cy.get('#capitalRevenueTypeSurplus').should('be.checked')
}

static submitPreviousFinancialYr()
{
    cy.get('input[type="submit"]').click()
}

}