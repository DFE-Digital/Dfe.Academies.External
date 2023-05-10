import BasePage from "../BasePage"
export default class A2BLeasesSummary extends BasePage {


static leasesSummaryElementsVisible()
{
    cy.leasesSummaryElementsVisible()
}

static submitLeasesSummary()
{
    cy.submitLeasesSummary()
}

}