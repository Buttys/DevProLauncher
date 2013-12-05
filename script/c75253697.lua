--Ｎｏ.７２ ラインモンスター チャリオッツ・飛車
function c75253697.initial_effect(c)
	--xyz summon
	aux.AddXyzProcedure(c,aux.XyzFilterFunction(c,6),2)
	c:EnableReviveLimit()
	--destroy
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(75253697,0))
	e1:SetCategory(CATEGORY_DESTROY)
	e1:SetType(EFFECT_TYPE_IGNITION)
	e1:SetRange(LOCATION_MZONE)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetCountLimit(1)
	e1:SetCost(c75253697.spcost)
	e1:SetTarget(c75253697.destg)
	e1:SetOperation(c75253697.desop)
	c:RegisterEffect(e1)
end
function c75253697.spcost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return e:GetHandler():CheckRemoveOverlayCard(tp,2,REASON_COST) end
	e:GetHandler():RemoveOverlayCard(tp,2,2,REASON_COST)
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_CHANGE_DAMAGE)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
	e1:SetTargetRange(0,1)
	e1:SetValue(c75253697.rdval)
	e1:SetReset(RESET_PHASE+PHASE_END)
	Duel.RegisterEffect(e1,tp)
end
function c75253697.rdval(e,re,val,r,rp,rc)
	if bit.band(r,REASON_BATTLE)~=0 then
		return val/2
	else
		return val
	end
end
function c75253697.filter1(c)
	return c:IsFaceup() and c:IsDestructable()
end
function c75253697.filter2(c)
	return c:IsFacedown() and c:IsDestructable()
end
function c75253697.destg(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return false end
	if chk==0 then return Duel.IsExistingTarget(c75253697.filter1,tp,0,LOCATION_MZONE,1,nil)
		and Duel.IsExistingTarget(c75253697.filter2,tp,0,LOCATION_SZONE,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_DESTROY)
	local g1=Duel.SelectTarget(tp,c75253697.filter1,tp,0,LOCATION_MZONE,1,1,nil)
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_DESTROY)
	local g2=Duel.SelectTarget(tp,c75253697.filter2,tp,0,LOCATION_SZONE,1,1,nil)
	g1:Merge(g2)
	Duel.SetOperationInfo(0,CATEGORY_DESTROY,g1,2,0,0)
end
function c75253697.desop(e,tp,eg,ep,ev,re,r,rp)
	local g=Duel.GetChainInfo(0,CHAININFO_TARGET_CARDS)
	local tc1=g:GetFirst()
	local tc=g:GetNext()
	if tc:IsRelateToEffect(e) and tc:IsFaceup() and tc1:IsRelateToEffect(e) and tc1:IsFacedown() then
		Duel.Destroy(tc,REASON_EFFECT)
		Duel.Destroy(tc1,REASON_EFFECT)
	end
end
