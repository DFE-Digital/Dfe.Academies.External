import BasePage from ../basePage
export default class A2BLeasesSummary extends BasePage {


static leasesSummaryElementsVisible()
{
    cy.leasesSummaryElementsVisible()
}

static leasesSelectOptionNo()
{
    cy.get('#anyLeasesOptionNo').click()
    cy.get('#anyLeasesOptionNo').should('be.checked')
}

static submitLeasesSummary()
{
    cy.submitLeasesSummary()
}

}