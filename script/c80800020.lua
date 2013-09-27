--森羅の仙樹 レギア
function c80800020.initial_effect(c)
	--deck check
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80800020,0))
	e1:SetType(EFFECT_TYPE_IGNITION)
	e1:SetCountLimit(1)
	e1:SetRange(LOCATION_MZONE)
	e1:SetTarget(c80800020.target)
	e1:SetOperation(c80800020.operation)
	c:RegisterEffect(e1)
	--sort decktop
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80800020,1))
	e2:SetProperty(EFFECT_FLAG_DAMAGE_STEP+EFFECT_FLAG_CARD_TARGET+EFFECT_FLAG_DELAY)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetCondition(c80800020.sdcon)
	e2:SetTarget(c80800020.sdtg)
	e2:SetOperation(c80800020.sdop)
	c:RegisterEffect(e2)
end
function c80800020.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFieldGroupCount(tp,LOCATION_DECK,0)>0 end
end
function c80800020.operation(e,tp,eg,ep,ev,re,r,rp)
	if Duel.GetFieldGroupCount(tp,LOCATION_DECK,0)==0 then return end
	Duel.ConfirmDecktop(tp,1)
	local g=Duel.GetDecktopGroup(tp,1)
	local tc=g:GetFirst()
	if tc:IsRace(RACE_PLANT) and tc:IsAbleToHand() then
		Duel.DisableShuffleCheck()
		Duel.SendtoGrave(g,nil,REASON_EFFECT)
		Duel.BreakEffect()
		Duel.Draw(tp,1,REASON_EFFECT)
	else
		Duel.MoveSequence(tc,1)
	end
end
function c80800020.sdcon(e,tp,eg,ep,ev,re,r,rp)
	if not re or e:GetHandler():IsReason(REASON_RETURN) then return false end
	local code=re:GetHandler():GetCode()
	return e:GetHandler():IsPreviousLocation(LOCATION_DECK) and (code==32362575 or code==79106360 or code==18631392 or code==58577036 or code==43040603 or code==22796548 or code==90951921 or code==80800020 or code==80800015 or code==1015)
end
function c80800020.sdtg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFieldGroupCount(tp,LOCATION_DECK,0)>0 end
end
function c80800020.sdop(e,tp,eg,ep,ev,re,r,rp)
	local ct=Duel.GetFieldGroupCount(tp,LOCATION_DECK,0)
	if ct==1 then 
		Duel.SortDecktop(tp,tp,1)
		e:SetLabel(1)
	else
		local ac=0
		Duel.Hint(HINT_SELECTMSG,tp,aux.Stringid(80800020,2))
		if ct==2 then ac=Duel.AnnounceNumber(tp,1,2)
		else ac=Duel.AnnounceNumber(tp,1,2,3) end
		Duel.SortDecktop(tp,tp,ac)
		e:SetLabel(ac)
	end
end
