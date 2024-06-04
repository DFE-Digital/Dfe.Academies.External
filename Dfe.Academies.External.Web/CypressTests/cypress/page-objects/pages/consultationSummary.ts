class ConsultationSummary {
  public consultationSummaryElementsVisible(): this {
    cy.consultationSummaryElementsVisible()

    return this
  }

  public selectConsultationStartSection(): this {
    cy.selectConsultationStartSection()

    return this
  }

  public consultationSummaryCompleteElementsVisible(): this {
    cy.consultationSummaryCompleteElementsVisible()

    return this
  }

  public submitConsultationSummary(): this {
    cy.submitConsultationSummary()

    return this
  }
}

const consultationSummary = new ConsultationSummary()

export default consultationSummary
