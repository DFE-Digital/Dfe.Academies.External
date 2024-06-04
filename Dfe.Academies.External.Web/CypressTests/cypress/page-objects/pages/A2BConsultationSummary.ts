import BasePage from ../basePage
export default class A2BConsultationSummary extends BasePage {
    
static consultationSummaryElementsVisible()
{
    cy.consultationSummaryElementsVisible()    
}

static selectConsultationStartSection()
{
    cy.selectConsultationStartSection()
}

static consultationSummaryCompleteElementsVisible()
{
    cy.consultationSummaryCompleteElementsVisible()
}

static submitConsultationSummary()
{
    cy.submitConsultationSummary()
}

}