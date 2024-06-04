class FuturePupilNumbersDetails {
  public futurePupilNumbersDetailsElementsVisible(): this {
    cy.futurePupilNumbersDetailsElementsVisible()

    return this
  }

  public fillFuturePupilNumbersDetails(): this {
    cy.fillFuturePupilNumbersDetails()

    return this
  }

  public submitFuturePupilNumbersDetails(): this {
    cy.submitFuturePupilNumbersDetails()

    return this
  }
}

const futurePupilNumbersDetails = new FuturePupilNumbersDetails()

export default futurePupilNumbersDetails
