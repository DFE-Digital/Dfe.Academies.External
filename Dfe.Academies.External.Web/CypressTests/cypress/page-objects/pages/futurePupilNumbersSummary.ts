class FuturePupilNumbersSummary {
  public futurePupilNumbersSummaryElementsVisible(): this {
    cy.futurePupilNumbersSummaryElementsVisible()

    return this
  }

  public selectFuturePupilNumbersStartSection(): this {
    cy.selectFuturePupilNumbersStartSection()

    return this
  }

  public futurePupilNumbersSummaryCompleteElementsVisible(): this {
    cy.futurePupilNumbersSummaryCompleteElementsVisible()

    return this
  }

  public submitFuturePupilNumbersSummary(): this {
    cy.submitFuturePupilNumbersSummary()

    return this
  }
}

const futurePupilNumbersSummary = new FuturePupilNumbersSummary()

export default futurePupilNumbersSummary
