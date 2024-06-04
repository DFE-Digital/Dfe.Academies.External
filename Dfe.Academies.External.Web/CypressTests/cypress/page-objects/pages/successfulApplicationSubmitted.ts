class SuccessfulApplicationSubmitted {
  public applicationSubmittedSuccessfullyElementsVisible(): this {
    cy.applicationSubmittedSuccessfullyElementsVisible()

    return this
  }
}

const successfulApplicationSubmitted = new SuccessfulApplicationSubmitted()

export default successfulApplicationSubmitted
