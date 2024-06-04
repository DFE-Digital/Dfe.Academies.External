import BasePage from ../basePage
export default class A2BJAMTrustDetailsSummary extends BasePage {
    
static JAMTrustDetailsSummaryElementsVisible() 
{
    cy.JAMTrustDetailsSummaryElementsVisible()
}

static JAMTrustDetailsSummarySelectStartSection() 
{
    cy.get('a[class=govuk-button govuk-button--secondary]').click()
}

static JAMTrustDetailsSummarySaveAndReturnToApp() 
{
    cy.JAMTrustDetailsSummarySaveAndReturnToApp()
}

}