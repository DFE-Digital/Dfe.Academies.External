import BasePage from "../BasePage"
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